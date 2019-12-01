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

            driver.Context.TranslationUnitPasses.RenameWithPattern("(^FRIBIDI_JOINING_TYPE_([A-Z])$)", "$2", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_JOINING_TYPE_JUNK", "Junk", RenameTargets.EnumItem);

            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_SET_NOT_FOUND", "NotFound", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_SET_UTF8", "UTF8", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_SET_CAP_RTL", "CapRtl", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_SET_ISO8859_6", "ISO8859_6", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_SET_ISO8859_8", "ISO8859_8", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_SET_CP1255", "CP1255", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_SET_CP1256", "CP1256", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_SETS_NUM_PLUS_ONE", "NumPlusOne", RenameTargets.EnumItem);

            driver.Context.TranslationUnitPasses.RenameWithPattern("(^FRIBIDI_CHAR_([A-Z]+)$)", "$2", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_HEBREW_ALEF", "HebrewAlef", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_ARABIC_ALEF", "ArabicAlef", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_ARABIC_ZERO", "ArabicZero", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_CHAR_PERSIAN_ZERO", "PersianZero", RenameTargets.EnumItem);

            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_FLAG_SHAPE_MIRRORING", "ShapeMirroring", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_FLAG_REORDER_NSM", "ReorderNsm", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_FLAG_SHAPE_ARAB_PRES", "ArabPres", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_FLAG_SHAPE_ARAB_LIGA", "ArabLiga", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_FLAG_SHAPE_ARAB_CONSOLE", "ArabConsole", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_FLAG_REMOVE_BIDI", "RemoveBidi", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_FLAG_REMOVE_JOINING", "RemoveJoining", RenameTargets.EnumItem);
            driver.Context.TranslationUnitPasses.RenameWithPattern("FRIBIDI_FLAG_REMOVE_SPECIALS", "RemoveSpecials", RenameTargets.EnumItem);

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
                "FRIBIDI_PAR_WLTR",
                "FRIBIDI_PAR_WRTL",
                "FRIBIDI_PAR_LTR",
                "FRIBIDI_PAR_RTL",
                "FRIBIDI_PAR_ON",
                "FRIBIDI_TYPE_BS",
                "FRIBIDI_TYPE_SS",
            };
        }
    }
}
