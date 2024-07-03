package vua.oicar.webfrontend.models;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.Optional;

@AllArgsConstructor
@NoArgsConstructor
@Setter
@Getter
public class LoginSession {
    private Optional<User> user;

    private Optional<String> token = Optional.empty();
}
