using System.Text;
using System.Transactions;

namespace Estore.BL.General
{

    public static class Helpers
    {
        public static TransactionScope CreateTransactionScope(int seconds = 60)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled);
        }

        public static Guid? StringToGuidDef(string str)
        {
            if (Guid.TryParse(str, out Guid value))
            {
                return value;
            }
            return null;
        }

        public static string Translit(string name)
        {
            var dict = new Dictionary<char, string>
                { {'а', "a"}, {'б', "b"}, {'в', "v"},{'г', "g" }, {'д', "d"}, {'е', "e"}, {'ё', "e"},
                {'ж', "gh"}, {'з', "z"}, {'и', "i"}, {'й', "y"}, {'к', "k"}, {'л',"l"}, {'м',"m"},
                {'н', "n"}, {'о',"o"}, {'п',"p"}, {'р',"r"}, {'с',"s"}, {'т',"t"}, {'у',"u"}, {'ф',"f"},
                {'х', "h"}, {'ц', "c"}, {'ч',"ch" }, {'ш',"sh"}, {'щ',"sch"}, {'э', "e"}, {'ю',"yu"},
                {'ы', "i"}, {'я', "ya"}};
            var stringBuilder = new StringBuilder();
            foreach (var c in name.ToLowerInvariant())
            {
                if (c >= '0' && c <= '9' || c >= 'a' && c <= 'z')
                    stringBuilder.Append(c);
                else if (dict.ContainsKey(c))
                    stringBuilder.Append(dict[c]);
                else stringBuilder.Append('-');
            }
            return stringBuilder.ToString().Replace("--", "-");
        }
    }
}
