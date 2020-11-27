#nullable enable
using System.Collections.Generic;

namespace School_Core.Util
{
    public class Result
    {
        public bool isSuccess { get; private set; } = true;
        public List<KeyErrorPair> Errors { get; private set; } = new List<KeyErrorPair>();

        private Result()
        {
            
        }

        public static Result Fail(string? key, string error)
        {
            return Fail(new List<KeyErrorPair>() {new KeyErrorPair(key, error)});
        }

        public static Result Fail(List<KeyErrorPair> errors)
        {
            var result = new Result();
            result.isSuccess = false;
            foreach (var entry in errors)
            {
                result.Errors.Add(new KeyErrorPair(entry.Key, entry.Error));
            }

            return result;
        }

        public static Result Success()
        {
            return new Result();
        }

        public class KeyErrorPair
        {
            public string Key { get; private set; }
            public string Error { get; private set; }

            public KeyErrorPair(string? key, string error)
            {
                Key = key ?? "";
                Error = error;
            }
        }
    }
}