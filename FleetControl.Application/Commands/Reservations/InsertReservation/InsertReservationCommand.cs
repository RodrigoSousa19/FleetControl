using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.InsertReservation
{
    public class InsertReservationCommand : IRequest<ResultViewModel<Reservation>>
    {
        public int IdProject { get; set; }
        public int IdDriver { get; set; }
        public int IdVehicle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Observation { get; set; }

        public Reservation ToEntity() => new(IdProject, IdDriver, IdVehicle, StartDate, EndDate, Observation);
    }
}
