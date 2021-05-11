using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Translators
{
  interface ITranslator<TDto,TEntity>
  {
    TDto EntityToDto(TEntity entity);
    TEntity DtoToEntity(TDto dto);
  }
}
