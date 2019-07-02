using System.Collections.Generic;
using System.Threading.Tasks;

namespace YouSign
{
    public interface IYouSignService
    {
        Task<FileOutput> FilePostAsync(FileInput file);

        Task<FileOutput> FileGetAsync(string id);

        Task<string> FileDownloadAsync(string id);

        Task<ProcedureOutput> ProcedurePostAsync(ProcedureInput input);

        Task DeleteAsync(string id);

        Task<StampOutput> ServerStampsPostAsync(StampInput cachet);

        Task<List<SignatureUIOutput>> SignaturesUIGetAsync();

        Task<SignatureUIInput> SignaturesUIPostAsync(SignatureUIInput input);

        Task<Layout> GetLayoutAsync(string fileId);
    }
}
