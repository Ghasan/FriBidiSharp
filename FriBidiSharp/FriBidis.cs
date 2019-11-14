using CppSharp;
using CppSharp.AST;
using CppSharp.Passes;
using System;
using System.IO;

namespace FriBidiSharp
{
    public class FriBidi : ILibrary
    {
        public void Setup(Driver driver)
        {
            var options = driver.Options;
            var module = options.AddModule("fribidi");
            module.Headers.Add("fribidi.h");
            options.OutputDir = "fribidi";

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
        }

        public void Preprocess(Driver driver, ASTContext ctx)
        {
            ctx.SetNameOfEnumWithMatchingItem("FRIBIDI_JOINING_TYPE_U", "JoiningType");
            ctx.SetNameOfEnumWithMatchingItem("FRIBIDI_CHAR_SET_UTF8", "CharSet");

            ctx.GenerateEnumFromMacros("Flags", "FRIBIDI_FLAG_(.*)").SetFlags();
            ctx.GenerateEnumFromMacros("FriBidiChar", "FRIBIDI_CHAR_(.*)");
        }

        public void Postprocess(Driver driver, ASTContext ctx)
        {
        }

        private static string getNativeLibraryPath()
        {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(userProfile, @"vcpkg\installed\x64-windows\include\fribidi");
        }

        private static string toPascalCase(string name)
        {
            string[] parts = name.Split('_');

            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];

                part = part.Insert(0, part.Substring(0, 1).ToUpperInvariant())
                    .Remove(1, 1);

                part = part.Insert(1, part.Substring(1).ToLowerInvariant())
                    .Remove(part.Length, part.Length - 1);

                parts[i] = part;
            }

            return String.Join(String.Empty, parts);
        }
    }
}
