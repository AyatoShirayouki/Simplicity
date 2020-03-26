using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Simplicity.DataContracts
{
    public enum TaskStatus
    {
        [Description("To Do")]
        ToDo,
        [Description("In Progress")]
        InProgress,
        [Description("In Review")]
        InReview,
        [Description("QA To Do")]
        QaToDo,
        [Description("QA In Progress")]
        QaInprogress,
        [Description("Done")]
        Done
    }
}
