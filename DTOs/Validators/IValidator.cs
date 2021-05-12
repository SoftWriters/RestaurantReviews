using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Validators
{
  public interface IValidator<T>
  {
    void ValidateData(T poco);
  }
}
