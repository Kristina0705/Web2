package ru.monegov.controller;

import io.micronaut.http.annotation.Body;
import io.micronaut.http.annotation.Controller;
import io.micronaut.http.annotation.Post;
import jakarta.inject.Inject;
import ru.monegov.authentication.AuthenticationService;
import ru.monegov.dto.CurrentUserDTO;
import ru.monegov.entity.UserEntity;

import javax.annotation.security.PermitAll;

@PermitAll
@Controller
public class RegisterController {

	@Inject
	private AuthenticationService authenticationService;

	@Post("/register")
	public CurrentUserDTO register(@Body UserEntity candidate) {
		String username = authenticationService.register(candidate);
		CurrentUserDTO user = new CurrentUserDTO();
		user.setUsername(username);
		return user;
	}
}
