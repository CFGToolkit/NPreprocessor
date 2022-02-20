﻿using NPreprocessor.Input;
using NPreprocessor.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NPreprocessor.Macros
{
    public class IfNDefMacro : IMacro
    {
        public IfNDefMacro()
        {
        }

        public string Pattern => "ifndef";

        public bool AreArgumentsRequired => true;
        
        public Task<(List<TextBlock> result, bool finished)> Invoke(ITextReader reader, State state)
        {
            var call = CallParser.GetInvocation(reader, 0, state.Definitions);
            reader.Current.Advance(call.length);
            var args = call.args;
            var name = MacroString.Trim(args[0]);

            if (!state.Definitions.Contains(name))
            {
                return Task.FromResult((MacroString.GetBlocks(args[1], state.NewLineEnding), true));
            }
            else
            {
                if (args.Length == 3)
                {
                    return Task.FromResult((MacroString.GetBlocks(args[2], state.NewLineEnding), true));
                }
                else
                {
                    return Task.FromResult((new List<TextBlock>(), true));
                }
            }
        }
    }
}
