using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Passes;
using System;
using System.IO;
using System.Linq;

namespace FriBidiSharpGenerator
{
    internal class Library : ILibrary
    {
        public void Setup(Driver driver)
        {
            var options = driver.Options;
            var module = options.AddModule("FriBidi");
            module.Headers.Add("fribidi.h");
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

            driver.Context.TranslationUnitPasses.AddPass(new CleanupPass(driver.Generator));
        }

        public void Preprocess(Driver driver, ASTContext ctx)
        {
            ctx.SetNameOfEnumWithMatchingItem("FRIBIDI_JOINING_TYPE_U", "JoiningType");
            ctx.SetNameOfEnumWithMatchingItem("FRIBIDI_CHAR_SET_UTF8", "CharSet");

            ctx.GenerateEnumFromMacros("Flags", "FRIBIDI_FLAG_(.*)").SetFlags();
            ctx.GenerateEnumFromMacros("NamedChar", "FRIBIDI_CHAR_(.*)");

            driver.Context.TranslationUnitPasses.Passes.RemoveAll(i => i.GetType() == typeof(MarshalPrimitivePointersAsRefTypePass));
        }

        public void Postprocess(Driver driver, ASTContext ctx)
        {
        }

        private class CleanupPass : TranslationUnitPass
        {
            private Generator _generator;
            private bool _added;

            public CleanupPass(Generator generator)
            {
                _generator = generator;
            }

            public override Boolean VisitTranslationUnit(TranslationUnit unit)
            {
                if (!base.VisitTranslationUnit(unit))
                    return false;

                if (!_added)
                {
                    _added = true;
                    _generator.OnUnitGenerated += onUnitGenerated;
                }

                return true;
            }

            public override bool VisitVariableDecl(Variable variable)
            {
                if (!base.VisitVariableDecl(variable))
                    return false;

                if (variable.LogicalOriginalName.StartsWith("fribidi_"))
                    variable.Ignore = true;

                return true;
            }

            private void onUnitGenerated(GeneratorOutput generatorOutput)
            {
                var functionBlocks = generatorOutput.Outputs.SelectMany(i => i.FindBlocks(BlockKind.Functions));
                var namespaces = generatorOutput.Outputs.SelectMany(i => i.FindBlocks(BlockKind.Namespace));

                foreach (var functionBlock in functionBlocks)
                    functionBlock.Blocks[0].Text.StringBuilder.Replace("public", "internal");

                foreach (var @namespace in namespaces.SelectMany(i => i.FindBlocks(BlockKind.Unknown)))
                    @namespace.Text.StringBuilder.Replace("FriBidi", "FriBidiSharp");
            }

        }

        private static string getNativeLibraryPath()
        {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string[] ids = { "x86-windows", "x64-windows", "x86-linux", "x64-linux" };

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
    }
}
