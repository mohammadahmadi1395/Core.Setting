namespace Alsahab.Common
{
    public interface IBaseTreeDTO : IBaseDTO
    {
        string Code { get; set; }
        long? Depth { get; set; }
        long? LeftIndex { get; set; }
        string OldCode { get; set; }
        long? ParentID { get; set; }
        long? RightIndex { get; set; }
        string Title { get; set; }
    }
}