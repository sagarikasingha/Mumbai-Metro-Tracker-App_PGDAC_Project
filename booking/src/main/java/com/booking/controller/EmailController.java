package com.booking.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.server.ResponseStatusException;

import com.booking.service.EmailService;
import com.booking.service.OTPService;

import jakarta.mail.MessagingException;

@CrossOrigin(origins = "*")
@RestController
public class EmailController {

    @Autowired
    private EmailService emailService;

    @Autowired
    private OTPService otpService;
    
    private String lastGeneratedOTP; // Variable to store last generated OTP

    @GetMapping("/send-test-email")
    public ResponseEntity<String> sendTestEmail(@RequestParam String to) {
        try {
            // Generate the OTP
            lastGeneratedOTP = otpService.generateOTP();

            // Email body with OTP
            String emailBody = "This is a test email to check the EmailService. Your OTP is: " + lastGeneratedOTP;

            // Send the email with OTP
            emailService.sendEmail(to, "Test Email with OTP", emailBody);

            return ResponseEntity.ok("Test email sent successfully to: " + to + " with OTP.");
        } catch (MessagingException e) {
            throw new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Failed to send test email", e);
        }
    }

    @PostMapping("/verify-otp")
    public ResponseEntity<String> verifyOTP(@RequestBody VerifyOTPRequest request) {
        String enteredOTP = request.getEnteredOTP();
        
        if (lastGeneratedOTP == null) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("No OTP generated yet. Please resend OTP.");
        }

        // Log the entered and last generated OTP for debugging
        System.out.println("Entered OTP: " + enteredOTP);
        System.out.println("Last Generated OTP: " + lastGeneratedOTP);

        // Validate the entered OTP
        boolean isValidOTP = otpService.validateOTP(enteredOTP, lastGeneratedOTP);

        if (isValidOTP) {
            // Correct OTP
            return ResponseEntity.ok("Entered OTP is valid.");
        } else {
            // Incorrect OTP
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body("Entered OTP is invalid.");
        }
    }

    // Class to map JSON request body
    public static class VerifyOTPRequest {
        private String enteredOTP;

        public String getEnteredOTP() {
            return enteredOTP;
        }

        public void setEnteredOTP(String enteredOTP) {
            this.enteredOTP = enteredOTP;
        }
    }
}

