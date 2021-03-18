using System.Collections.Generic;

namespace PackagesExplorer.Library
{
    public class ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public bool Ok { get; set; }

        public static ApiResponse Success()
        {
            return new()
            {
                Ok = true,
            };
        }

        public static ApiResponse Failed(string[] errors)
        {
            return new()
            {
                Ok = false,
                Errors = errors,
            };
        }
    }

    public class ApiResponse<TResource> : ApiResponse
    {
        public TResource Resource { get; set; }

        public new static ApiResponse<TResource> Success()
        {
            return new()
            {
                Ok = true,
            };
        }

        public static ApiResponse<TResource> Success(TResource resource)
        {
            return new()
            {
                Ok = true,
                Resource = resource,
            };
        }
    }

    public class ApiCollectionResponse<TResource> : ApiResponse<IEnumerable<TResource>>
    {
        public static ApiCollectionResponse<TResource> Success(IEnumerable<TResource> resource)
        {
            return new ApiCollectionResponse<TResource>()
            {
                Resource = resource,
                Ok = true,
            };
        }
    }
}
