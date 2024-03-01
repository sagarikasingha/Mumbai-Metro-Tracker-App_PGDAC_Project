package com.booking.service;

import java.util.Random;

//import org.slf4j.Logger;
//import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

@Service
public class OTPService {

   // private static final Logger logger = LoggerFactory.getLogger(OTPService.class);

    public String generateOTP() {
        Random random = new Random();
        int otp = 100000 + random.nextInt(900000); // Generate a 6-digit OTP
        return String.valueOf(otp);
    }

    public boolean validateOTP(String enteredOTP, String actualOTP) {
    	//        logger.debug("Entered OTP: {}", enteredOTP);
    	//        logger.debug("Actual OTP: {}", actualOTP);

        return enteredOTP.equals(actualOTP);
    }
}