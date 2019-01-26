using System.ComponentModel;

namespace FormotsCommon.FormValidation
{
    public abstract class DataErrorInfo : IDataErrorInfo
    {
        string IDataErrorInfo.Error => DataErrorInfoHelper.GetErrorInfo(this);

        string IDataErrorInfo.this[string columnName] => DataErrorInfoHelper.GetErrorInfo(this, columnName);
    }
}