using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.User;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.InsertDriver
{
    public class InsertDriverCommand : IRequest<ResultViewModel<Driver>>
    {
        public int IdUser { get; set; }
        public string DocumentNumber { get; set; }
        public DocumentType DocumentType { get; set; }

        public Driver ToEntity() => new(IdUser, DocumentNumber, DocumentType);
    }
}
