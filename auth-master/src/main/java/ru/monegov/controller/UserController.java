package ru.monegov.controller;

import io.micronaut.http.annotation.Controller;
import io.micronaut.http.annotation.Get;
import io.micronaut.security.annotation.Secured;
import io.micronaut.security.authentication.Authentication;
import io.micronaut.security.rules.SecurityRule;
import ru.monegov.dto.CurrentUserDTO;

@Secured(SecurityRule.IS_AUTHENTICATED)
@Controller
public class UserController {
	@Get("/current-user")
	public CurrentUserDTO currentUser(Authentication authentication){
		CurrentUserDTO currentUser = new CurrentUserDTO();
		currentUser.setUsername(authentication.getName());
		return  currentUser;
	}
}
