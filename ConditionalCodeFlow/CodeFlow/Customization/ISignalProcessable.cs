using System.Collections;
using System.Collections.Generic;

namespace CodeFlow.Customization
{
    public interface ISignalProcessable
    {
        Signal ProcessSignal(IEnumerable<Signal> signals);
    }
}