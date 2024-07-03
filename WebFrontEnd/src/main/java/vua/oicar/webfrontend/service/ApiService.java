package vua.oicar.webfrontend.service;

import vua.oicar.webfrontend.models.*;

import java.util.HashMap;
import java.util.List;
import java.util.Optional;

public interface ApiService {

    Optional<String> login(LoginRequest request);
    boolean register(RegisterRequest request);
    Optional<User> getUserInfo(LoginSession session);
    boolean isValidSession(LoginSession session);

    boolean deleteUser(LoginSession session);

    List<Language> getLanguages();

    List<LanguageStat> getLanguageStats(LoginSession session, int id);
}
