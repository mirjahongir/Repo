
namespace Repo.Exception
{
    public class RepoException:System.Exception
    {
        public int Code { get; set; }
        public int Status { get; set; }
        public object Data { get; set; }

        public RepoException()
        {

        }
        public RepoException(int code):this()
        {
            Code = code;
        }
        public RepoException(int code, int status):this(code)
        {
            Status = status;
        }
        public RepoException(int code, int status, object data) :this(code, status)
        {
            Data = data;
        }


    }
}
