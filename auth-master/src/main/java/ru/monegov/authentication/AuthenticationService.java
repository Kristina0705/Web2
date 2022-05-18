package ru.monegov.authentication;

import jakarta.inject.Inject;
import jakarta.inject.Singleton;
import ru.monegov.entity.UserEntity;
import ru.monegov.repository.UserRepository;

import javax.transaction.Transactional;
import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.util.Arrays;
import java.util.Base64;
import java.util.Optional;

@Singleton
public class AuthenticationService {
	@Inject
	private UserRepository userRepository;

	private final SecureRandom random = new SecureRandom();

	@Transactional
	public String register(UserEntity user) {
		String username = user.getUsername();
		if (userRepository.findById(username).isPresent()) {
			return null;
		}
		String password = user.getPassword();
		byte[] salt = getSalt();
		user.setSalt(encodeB64(salt));
		MessageDigest md;
		try {
			md = MessageDigest.getInstance("SHA-512");
		} catch (NoSuchAlgorithmException e) {
			return null;
		}
		md.update(salt);
		byte[] hashedPassword = md.digest(password.getBytes(StandardCharsets.UTF_8));
		user.setPassword(encodeB64(hashedPassword));
		return userRepository.save(user).getUsername();
	}

	public boolean passwordMath(String username, String password) {
		Optional<UserEntity> candidate = userRepository.findById(username);
		if (candidate.isEmpty()) {
			return false;
		}
		UserEntity user = candidate.get();
		byte[] salt = decodeB64(user.getSalt());
		byte[] hashedPassword = decodeB64(user.getPassword());
		MessageDigest md;
		try {
			md = MessageDigest.getInstance("SHA-512");
		} catch (NoSuchAlgorithmException e) {
			return false;
		}
		md.update(salt);
		byte[] toCheck = md.digest(password.getBytes(StandardCharsets.UTF_8));
		return Arrays.equals(toCheck, hashedPassword);
	}

	private byte[] getSalt() {
		byte[] salt = new byte[16];
		random.nextBytes(salt);
		return salt;
	}

	private String encodeB64(byte[] data) {
		return Base64.getEncoder().encodeToString(data);
	}

	private byte[] decodeB64(String data) {
		return Base64.getDecoder().decode(data);
	}
}
