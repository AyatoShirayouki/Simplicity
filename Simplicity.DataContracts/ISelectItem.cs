using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.DataContracts
{
    public interface ISelectItem
    {
        bool Selected { get; set; }
        bool Disabled { get; set; }
        string Text { get; set; }
        string Value { get; set; }
        string DataAttribute { get; set; }
    }
}
