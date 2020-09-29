import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Deck {
    private List<Card> cards = new ArrayList<>();
    private final int numOfCards = 52;

    // TODO initialize deck by iterating through lists
    public Deck() {
        for (int i = 0; i < numOfCards; i++) {
            Card newCard = new Card();

            // Assigns a suit
            switch (i % 4) {
                case 0 -> newCard.setSuit("Spades");
                case 1 -> newCard.setSuit("Clubs");
                case 2 -> newCard.setSuit("Hearts");
                case 3 -> newCard.setSuit("Diamonds");
            }

            // Assigns a rank
            switch (i % 13) {
                case 0 -> {
                    newCard.setRank("Ace");
                    newCard.setValue(11);
                }
                case 1 -> {
                    newCard.setRank("2");
                    newCard.setValue(2);
                }
                case 2 -> {
                    newCard.setRank("3");
                    newCard.setValue(3);
                }
                case 3 -> {
                    newCard.setRank("4");
                    newCard.setValue(4);
                }
                case 4 -> {
                    newCard.setRank("5");
                    newCard.setValue(5);
                }
                case 5 -> {
                    newCard.setRank("6");
                    newCard.setValue(6);
                }
                case 6 -> {
                    newCard.setRank("7");
                    newCard.setValue(7);
                }
                case 7 -> {
                    newCard.setRank("8");
                    newCard.setValue(8);
                }
                case 8 -> {
                    newCard.setRank("9");
                    newCard.setValue(9);
                }
                case 9 -> {
                    newCard.setRank("10");
                    newCard.setValue(10);
                }
                case 10 -> {
                    newCard.setRank("Jack");
                    newCard.setValue(10);
                }
                case 11 -> {
                    newCard.setRank("Queen");
                    newCard.setValue(10);
                }
                case 12 -> {
                    newCard.setRank("King");
                    newCard.setValue(10);
                }
            }

            // Set card abbreviation
            newCard.setAbbrev();

            // Add new card to the deck
            cards.add(newCard);
        }
    }

    public List<Card> getCards() {
        return cards;
    }

    public void setCards(List<Card> cards) {
        this.cards = cards;
    }

    public void addCard(Card card) {
        cards.add(card);
    }

    public void shuffle() {
        Collections.shuffle(cards);
    }
}