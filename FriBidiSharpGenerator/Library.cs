using CodingSeb.ExpressionEvaluator;
using CppSharp;
using CppSharp.AST;
using CppSharp.Passes;
using FribidiSharpGenerator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FriBidiSharpGenerator
{
    internal class Library : ILibrary
    {
        public void Setup(Driver driver)
        {
            var options = driver.Options;
            var module = options.AddModule("fribidi");
            module.Headers.Add("fribidi.h");
            module.OutputNamespace = "FriBidiSharp";
            options.OutputDir = getOutputDirectory();

            var parserOptions = driver.ParserOptions;
            parserOptions.AddIncludeDirs(getNativeLibraryPath());
        }

        public void SetupPasses(Driver driver)
        {
            driver.Context.TranslationUnitPasses.RemovePrefix("fribidi");

            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_JOINING_TYPE_(.*)", "$1", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_SETS?_(.*)", "$1", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_(.*)", "$1", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_FLAG_(.*)", "$1", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_TYPE_(.*)", "$1", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_PAR_(.*)", "$1", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_MASK_(.*)", "$1", RenameTargets.EnumItem);

            driver.Context.TranslationUnitPasses.AddPass(new CleanupPass());
            driver.Context.GeneratorOutputPasses.AddPass(new RenameOutputClasses());
        }

        public void Preprocess(Driver driver, ASTContext ctx)
        {
            ctx.SetNameOfEnumWithMatchingItem("FRIBIDI_JOINING_TYPE_U", "JoiningType");
            ctx.SetNameOfEnumWithMatchingItem("FRIBIDI_CHAR_SET_UTF8", "CharSet");

            ctx.GenerateEnumFromMacros("Flags", "FRIBIDI_FLAG_(.*)").SetFlags();
            ctx.GenerateEnumFromMacros("NamedChar", "FRIBIDI_CHAR_(.*)");

            foreach (var unit in ctx.TranslationUnits)
            {
                foreach (var macro in unit.PreprocessedEntities.OfType<MacroDefinition>())
                {
                    if (macro.Name.StartsWith("FRIBIDI") && Regex.IsMatch(macro.Expression, "(?i)^0x[0-9a-f]+L$"))
                    {
                        string num = macro.Expression.Substring(2);
                        num = num.TrimEnd('L');
                        num = UInt64.Parse(num, NumberStyles.HexNumber).ToString();
                        macro.Expression = num;
                    }
                }
            }

            ctx.GenerateEnumFromMacros("TypeMasks", "FRIBIDI_MASK_(.*)").SetFlags();
            ctx.GenerateEnumFromMacros("JoinMasks", getJoinMasks()).SetFlags();

            var evaluator = new ExpressionEvaluator();
            var tempMacros = new HashSet<MacroDefinition>();

            foreach (var unit in ctx.TranslationUnits)
            {
                foreach (var macro in unit.PreprocessedEntities.OfType<MacroDefinition>())
                {
                    if (Regex.IsMatch(macro.Name, "^FRIBIDI_(TYPE|PAR)"))
                    {
                        var typeMasks = ctx.FindEnum("TypeMasks").Single();
                        var joinMasks = ctx.FindEnum("JoinMasks").Single();

                        var exp = macro.Expression.Replace("\\", String.Empty);

                        foreach (Match refMacro in Regex.Matches(exp, @"FRIBIDI_\w+"))
                        {
                            var typeMask = typeMasks.Items.SingleOrDefault(i => i.Name == refMacro.Value);
                            var joinMask = joinMasks.Items.SingleOrDefault(i => i.Name == refMacro.Value);
                            var sibling = tempMacros.SingleOrDefault(i => i.Name == refMacro.Value);

                            if (typeMask != null)
                                exp = exp.Replace(refMacro.Value, typeMask.Value.ToString());
                            else if (joinMask != null)
                                exp = exp.Replace(refMacro.Value, joinMask.Value.ToString());
                            else if (sibling != null)
                                exp = exp.Replace(refMacro.Value, sibling.Expression);
                            else
                                throw new ArgumentOutOfRangeException();
                        }

                        exp = evaluator.Evaluate(exp).ToString();

                        macro.Expression = exp;

                        tempMacros.Add(macro);
                    }
                }
            }

            ctx.GenerateEnumFromMacros("Type", "FRIBIDI_TYPE_(.*)");
            ctx.GenerateEnumFromMacros("DeprecatedType", getDeprecatedTypes());
            ctx.GenerateEnumFromMacros("ParagraphType", "FRIBIDI_PAR_(.*)");

            driver.Context.TranslationUnitPasses.Passes.RemoveAll(i => i.GetType() == typeof(MarshalPrimitivePointersAsRefTypePass));
        }

        public void Postprocess(Driver driver, ASTContext ctx)
        {
        }

        // Private
        
        private static string getNativeLibraryPath()
        {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            // Relies on vcpkg on Windows to read fribidi.h
            string[] ids = { "x86-windows", "x64-windows" };

            foreach (string id in ids)
            {
                string path = Path.Combine(userProfile, $"vcpkg/installed/{id}/include/fribidi");

                if (Directory.Exists(path))
                    return path;
            }

            throw new FileNotFoundException("FriBidi headers not found");
        }

        private static string getOutputDirectory()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;

            while (!Path.GetFileName(dir).Equals("FriBidiSharp", StringComparison.InvariantCultureIgnoreCase))
                dir = Directory.GetParent(dir).FullName;

            return Path.Combine(dir, "FriBidiSharp");
        }

        private static string[] getJoinMasks()
        {
            return new string[]
            {
                "FRIBIDI_MASK_JOINS_RIGHT",
                "FRIBIDI_MASK_JOINS_LEFT",
                "FRIBIDI_MASK_ARAB_SHAPES",
                "FRIBIDI_MASK_TRANSPARENT",
                "FRIBIDI_MASK_IGNORED",
                "FRIBIDI_MASK_LIGATURED",
            };
        }

        private static string[] getDeprecatedTypes()
        {
            return new string[]
            {
                "FRIBIDI_TYPE_WL",
                "FRIBIDI_TYPE_WR",
                "FRIBIDI_TYPE_L",
                "FRIBIDI_TYPE_N",
                "FRIBIDI_TYPE_B",
                "FRIBIDI_TYPE_R",
                "FRIBIDI_TYPE_S",
            };
        }
    }
}
