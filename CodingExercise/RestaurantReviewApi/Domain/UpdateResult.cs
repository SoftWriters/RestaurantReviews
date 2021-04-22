using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UpdateResult
    {
        public UpdateResult() { this.success = true; errors = new List<string>(); }

        public UpdateResult(bool success, string error) { this.success = success; this.errors = new List<string>(); if (!success && error != null) this.errors.Add(error); }

        public UpdateResult(bool success, List<string> errors) { this.success = success; this.errors = errors; }

        public bool success { get; set; }

        public List<string> errors { get; set; }

    }
}
