using DAL.Entities;

namespace BLL.Generators.Base;
public interface ITicketGenerator
{
    List<Ticket> Generate(int count);
}
