﻿using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.ReservationsComments
{
    public class InsertReservationCommentCommand : IRequest<ResultViewModel<ReservationComment>>
    {
        public int IdUser { get; set; }
        public int IdReservation { get; set; }
        public string Content { get; set; }

        public ReservationComment ToEntity() => new(Content, IdReservation, IdUser);
    }
}
