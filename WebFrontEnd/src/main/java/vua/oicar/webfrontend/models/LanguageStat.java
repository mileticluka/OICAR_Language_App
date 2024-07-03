package vua.oicar.webfrontend.models;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.HashMap;

@AllArgsConstructor
@NoArgsConstructor
@Setter
@Getter
public class LanguageStat {
    private String name;
    private int score;


    private static HashMap<String, String> PRETTY_NAMES = new HashMap<>();
    static {
        PRETTY_NAMES.put("pick_sentence_played", "Played Pick Sentence Games");
        PRETTY_NAMES.put("pick_sentence_completed", "Completed Pick Sentence Games");
        PRETTY_NAMES.put("flash_cards_played", "Played Flash Cards Games");
        PRETTY_NAMES.put("flash_cards_completed", "Completed Flash Cards Games");
        PRETTY_NAMES.put("fill_blank_played", "Played Fill Blank Games");
        PRETTY_NAMES.put("fill_blank_completed", "Completed Fill Blank Games");
    }

    public String getPrettyName() {
        return PRETTY_NAMES.getOrDefault(name, name);
    }
}
