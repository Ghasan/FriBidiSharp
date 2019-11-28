﻿using CppSharp;
using CppSharp.AST;
using CppSharp.Passes;
using FribidiSharpGenerator;
using System;
using System.IO;

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
    }
}
