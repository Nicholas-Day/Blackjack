/**
 * The Card class represents a typical playing card
 *
 * @author Nicholas Day
 */
public class Card {
    private String rank;
    private String suit;
    private String abbrev;
    private int value;

    public String getRank() {
        return rank;
    }

    public void setRank(String rank) {
        this.rank = rank;
    }

    public String getSuit() {
        return suit;
    }

    public void setSuit(String suit) {
        this.suit = suit;
    }

    public String getAbbrev() {
        return abbrev;
    }

    public void setAbbrev() {
        if (rank.equals("10")) abbrev = ("T" + suit.charAt(0));
        else abbrev = (Character.toString(rank.charAt(0)) + suit.charAt(0));
    }

    public int getValue() {
        return value;
    }

    public void setValue(int value) {
        this.value = value;
    }
}