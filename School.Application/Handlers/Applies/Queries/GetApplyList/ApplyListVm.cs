namespace School.Application.Handlers.Applies.Queries.GetApplyList
{
    public class ApplyListVm
    {
        public IList<ApplyLookupDto> Applies { get; set; } = new List<ApplyLookupDto>();
    }
}
