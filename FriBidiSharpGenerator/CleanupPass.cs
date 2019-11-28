using CppSharp.AST;
using CppSharp.AST.Extensions;
using CppSharp.Passes;
using System.Linq;

namespace FribidiSharpGenerator
{
    public class CleanupPass : TranslationUnitPass
    {
        public override bool VisitVariableDecl(Variable variable)
        {
            if (!base.VisitVariableDecl(variable))
                return false;

            if (variable.LogicalOriginalName.StartsWith("fribidi_"))
                variable.Ignore = true;

            return true;
        }

        public override bool VisitFunctionDecl(Function function)
        {
            if (!base.VisitFunctionDecl(function))
                return false;

            if (function.LogicalOriginalName.StartsWith("fribidi_"))
            {
                for (int i = 0; i < function.Parameters.Count; i++)
                {
                    var param = function.Parameters[i];

                    if (param.Type.IsPointer() && !param.Type.GetPointee().IsPointer())
                    {
                        param.QualifiedType = new QualifiedType(new ArrayType()
                        {
                            SizeType = ArrayType.ArraySize.Incomplete,
                            QualifiedType = new QualifiedType((CppSharp.AST.Type)param.Type.GetPointee().Clone())
                        });
                    }
                    else if (param.LogicalOriginalName == "flags")
                    {
                        Enumeration flags = ASTContext.FindEnum("Flags").Single();
                        param.QualifiedType = new QualifiedType(new TagType(flags));
                    }
                }
            }

            return true;
        }
    }
}
