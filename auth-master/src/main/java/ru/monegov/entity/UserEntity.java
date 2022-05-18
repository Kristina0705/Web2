package ru.monegov.entity;

import io.micronaut.core.annotation.NonNull;
import io.micronaut.data.annotation.Id;
import io.micronaut.data.annotation.MappedEntity;
import javax.validation.constraints.NotBlank;
import javax.validation.constraints.NotNull;

@MappedEntity
public class UserEntity {
	@Id
	@NonNull
	private String username;

	@NonNull
	@NotBlank
	@NotNull
	private String password;

	@NonNull
	@NotBlank
	@NotNull
	private String salt;

	@NonNull
	public String getUsername() {
		return username;
	}

	public void setUsername(@NonNull String username) {
		this.username = username;
	}

	@NonNull
	public String getPassword() {
		return password;
	}

	public void setPassword(@NonNull String password) {
		this.password = password;
	}

	@NonNull
	public String getSalt() {
		return salt;
	}

	public void setSalt(@NonNull String salt) {
		this.salt = salt;
	}
}
