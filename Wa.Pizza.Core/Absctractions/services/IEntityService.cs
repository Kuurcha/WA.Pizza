using Microsoft.EntityFrameworkCore;

namespace Wa.Pizza.Infrasctructure.Services.Interfaces
{
    public interface IEntityService<T>
    {
        //Интерфейс только для множественной имплементации, либо мок данные в тестах
        T GetByGUID(int guid);
    }
}
