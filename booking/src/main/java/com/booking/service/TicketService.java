package com.booking.service;

import java.time.LocalTime;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.server.ResponseStatusException;

import com.booking.entity.RouteStation;
import com.booking.entity.Ticket;
import com.booking.repo.RouteStationRepository;
import com.booking.repo.TicketRepository;

@Service
public class TicketService {

    @Autowired
    private RouteStationRepository routeStationRepository;
    
    @Autowired
    private TicketRepository ticketRepository;

    public Ticket bookTicket(int routeId, int stationIdFrom, int stationIdTo) {
        RouteStation routeStation = routeStationRepository.findFirstByStationFromStationIdAndStationToStationId(stationIdFrom, stationIdTo);

        if (routeStation == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Route Station not found");
        }

        Ticket ticket = new Ticket();
        ticket.setStationIdFrom(routeStation.getStationFrom().getStationId());
        ticket.setStationIdTo(routeStation.getStationTo().getStationId());
        ticket.setFare(routeStation.getFare());
        ticket.setTime(LocalTime.now());
         
        @SuppressWarnings("unused")
		Ticket savedTicket = ticketRepository.save(ticket);
        
        return ticket;
    }

    public Ticket getTicketById(int ticketId) {
        return ticketRepository.findById(ticketId).orElse(null);
    }
}
