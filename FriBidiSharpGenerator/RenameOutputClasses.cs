using CppSharp;
using CppSharp.Generators;
using CppSharp.Passes;
using System.Linq;

namespace FribidiSharpGenerator
{
    public class RenameOutputClasses : GeneratorOutputPass
    {
        public override void VisitGeneratorOutput(GeneratorOutput output)
        {
            var unkowns = output.Outputs.SelectMany(i => i.FindBlocks(BlockKind.Unknown));

            foreach (var unkown in unkowns)
            {
                unkown.Text.StringBuilder.Replace("fribidi_begindecls", "Main");
                unkown.Text.StringBuilder.Replace("fribidi_common", "Others");
            }
        }
    }
}
