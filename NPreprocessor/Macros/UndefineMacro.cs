﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace NPreprocessor.Macros
{
    public class UndefineMacro : IMacro
    {
        public UndefineMacro()
        {
        }

        public string Pattern => "undefine";

        public bool AreArgumentsRequired => true;

        public Task<(List<TextBlock> result, bool finished)> Invoke(ITextReader reader, State state)
        {
            var call = CallParser.GetInvocation(reader, 0, state.Definitions);
            reader.Current.Consume(call.length);
            var args = call.args;
            var name = MacroString.Trim(args[0]);

            if (state.Mappings.ContainsKey(name))
            {
                state.Mappings.Remove(name);
            }
            return Task.FromResult((new List<TextBlock>() { string.Empty }, true));
        }
    }
}
