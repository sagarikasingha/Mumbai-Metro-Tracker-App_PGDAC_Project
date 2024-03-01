package com.booking.controller;

import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.booking.entity.Ticket;
import com.booking.service.TicketService;

@CrossOrigin(origins = "*")
@RestController
@RequestMapping("/tickets")
public class TicketController {

    @Autowired  
    private TicketService ticketService;
    /*Autowires (injects) an instance of TicketService into the controller.
     *This allows the controller to use the methods provided by TicketService 
     *to perform ticket-related operations.*/

    @PostMapping("/book")
    public ResponseEntity<Ticket> bookTicket(@RequestBody Map<String, Integer> request) {
        int routeId = request.get("routeId");
        int stationIdFrom = request.get("stationIdFrom");
        int stationIdTo = request.get("stationIdTo");

        Ticket ticket = ticketService.bookTicket(routeId, stationIdFrom, stationIdTo);
        return ResponseEntity.ok(ticket);
    }

    @GetMapping("/{ticketId}")
    public ResponseEntity<Ticket> getTicketById(@PathVariable int ticketId) {
        Ticket ticket = ticketService.getTicketById(ticketId);
        
        if (ticket == null) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        
        return new ResponseEntity<>(ticket, HttpStatus.OK);
    }
}
