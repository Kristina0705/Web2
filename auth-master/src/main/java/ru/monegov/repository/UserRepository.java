package ru.monegov.repository;

import io.micronaut.data.jdbc.annotation.JdbcRepository;
import io.micronaut.data.model.query.builder.sql.Dialect;
import io.micronaut.data.repository.CrudRepository;
import ru.monegov.entity.UserEntity;

import javax.transaction.Transactional;
import javax.validation.constraints.NotBlank;
import javax.validation.constraints.NotNull;

@JdbcRepository(dialect = Dialect.H2)
public interface UserRepository extends CrudRepository<UserEntity, String> {
	@Transactional
	UserEntity save(@NotNull @NotBlank String username,
	                @NotNull @NotBlank String password,
	                @NotNull @NotBlank String salt);
}
