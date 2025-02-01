using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.InsertReservation
{
    public class InsertReservationCommand : IRequest<ResultViewModel<Reservation>>
    {
        public int IdCustomer { get; private set; }
        public int IdDriver { get; private set; }
        public int IdVehicle { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Observation { get; private set; }

        public Reservation ToEntity() => new(IdCustomer, IdDriver, IdVehicle, StartDate, EndDate, Observation);
    }
}
