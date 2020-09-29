import java.util.ArrayList;
import java.util.List;

public class Hand {
    private int handValue;
    private int betAmount = 0;
    private List<Card> cards = new ArrayList<>();

    public int getHandValue() {
        return determineValue();
    }

    public void setHandValue(int handValue) {
        this.handValue = handValue;
    }

    public int getBetAmount() {
        return betAmount;
    }

    public void setBetAmount(int betAmount) {
        this.betAmount = betAmount;
    }

    public List<Card> getCards() {
        return cards;
    }

    public void setCards(List<Card> cards) {
        this.cards = cards;
    }

    public void addCardToHand(Card card) {
        cards.add(card);
    }

    public boolean hasAce() {
        for (Card card : cards) {
            if (card.getRank().equals("Ace")) {
                return true;
            }
        }
        return false;
    }

    public boolean hasPair() {
        return (cards.get(0).getValue() == cards.get(1).getValue());
    }

    public boolean is21() {
        return (handValue == 21);
    }

    public int numOfAces() {
        int numOfAces = 0;
        for (Card currentCard : cards) {
            if (currentCard.getRank().equals("Ace")) numOfAces += 1;
        }
        return numOfAces;
    }

    public int determineValue() {
        // Get default hand value
        handValue = 0;
        for (Card currentCard : cards) {
            handValue += currentCard.getValue();
        }

        if (!hasAce()) return handValue;

        // For each ace that makes the hand bust, make that ace have a value of 1 (default value 11)
        for (int i = 0; i < numOfAces(); i++) {
            if (handValue < 22) break;
            handValue = handValue - 10;
        }

        return handValue;
    }

    public void display(String name, boolean isDealerHand){
        if (isDealerHand){
            System.out.println("Dealer's cards: \n(xx) face down");
            Game.printCard(cards.get(0));
            System.out.println("Dealer is showing a hand value of: " + cards.get(0).getValue());
        }
        else {
            System.out.println(name + "'s cards:");
            for (Card currentCard : cards) {
                Game.printCard(currentCard);
            }
            System.out.println("The current value of " + name +"'s hand is: " + getHandValue() + "\n");
        }
    }
}