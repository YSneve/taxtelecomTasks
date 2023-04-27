using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace taxTelecomTasks.WebApi
{
    public class SortingModel
    {
        [EnumDataType(typeof(SortingEnum))] public SortingEnum Sorting { get; set; }
    }

    public enum SortingEnum
    {
        [EnumMember(Value = "Quick")] Quick,
        [EnumMember(Value = "Tree")] Tree
    }
}