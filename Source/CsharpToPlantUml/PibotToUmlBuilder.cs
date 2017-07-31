using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpToPlantUml
{
    public class PibotToUmlBuilder
    {
        public string Build(Pibot pibot)
        {
            StringBuilder sb = new StringBuilder();

            // ****************************
            // * アクセス修飾子（その１） *
            // ****************************
            //
            // ステレオタイプはここでは付けません
            // 名前の前にステレオタイプを付けると、インデントがぐちゃぐちゃになるんで、型の後ろに付けることにします
            switch (pibot.accessModify)
            {
                case Pibot.AccessModify.Private: sb.Append("- "); break;
                case Pibot.AccessModify.Protected: sb.Append("# "); break;
                case Pibot.AccessModify.ProtectedInternal: sb.Append("# "); break;
                case Pibot.AccessModify.Public: sb.Append("+ "); break;
            }

            bool writedColon = false;
            if ("" == pibot.name)
            {
                // 名前が無い場合、コンストラクタ
                // 名前
                sb.Append(pibot.type); // 型を名前扱いにする

                // 引数リスト
                if (0 < pibot.argumentList.Length) { sb.Append(pibot.argumentList.ToString()); }

                //sb.Append(" : ");
                //writedColon = true;
            }
            else
            {
                // 名前
                sb.Append(pibot.name);

                // 引数リスト
                if (0 < pibot.argumentList.Length) { sb.Append(pibot.argumentList.ToString()); }

                sb.Append(" : ");
                writedColon = true;
                // 型
                sb.Append(pibot.type);
                // ジェネリック型引数
                if (0 < pibot.genericParameters.Length) { sb.Append(pibot.genericParameters.ToString()); }
            }

            // **********
            // * 修飾子 *
            // **********
            //
            // 名前の前にステレオタイプ等を付けると、インデントがぐちゃぐちゃになるんで、型の後ろに付けることにします
            if (pibot.isStatic ||
                pibot.isConst ||
                pibot.isReadonly ||
                pibot.isOverride ||
                pibot.isVirtual ||
                pibot.accessModify == Pibot.AccessModify.Internal ||
                pibot.accessModify == Pibot.AccessModify.ProtectedInternal
                )
            {
                if (!writedColon)
                {
                    sb.Append(" : UNKNOWN_TYPE");
                    writedColon = true;
                }

                // ****************************
                // * アクセス修飾子（その２） *
                // ****************************
                //
                // 名前の前に付けると、インデントがぐちゃぐちゃになるんで、ステレオタイプは型の後ろに付けることにする
                switch (pibot.accessModify)
                {
                    case Pibot.AccessModify.Internal: sb.Append(" <<internal>>"); break;
                    case Pibot.AccessModify.ProtectedInternal: sb.Append(" <<internal>>"); break;
                }

                // 修飾子
                if (pibot.isStatic)
                {
                    sb.Append(" {static}");
                }
                // 修飾子
                if (pibot.isConst)
                {
                    sb.Append(" const");
                }
                // 修飾子
                if (pibot.isReadonly)
                {
                    sb.Append(" <<readonly>>");
                }
                // 修飾子
                if (pibot.isOverride)
                {
                    sb.Append(" <<override>>");
                }
                // 修飾子
                if (pibot.isVirtual)
                {
                    sb.Append(" <<virtual>>");
                }
            }

            if (0 < pibot.summaryComment.Length)
            {
                if (!writedColon)
                {
                    sb.Append(" : ");
                }
                sb.Append(" '");
                sb.Append(pibot.summaryComment.ToString().Trim());
                sb.Append("'");
            }

            return sb.ToString();
        }
    }
}
