import java.util.List;
import java.util.ArrayList;

public class Player {
    private String name;
    private List<Hand> hands = new ArrayList<>();
    private int initialBank = 0;
    private int currentBank = 0;
    private int bet = 0;
    private int sideBet = 0;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Hand> getHands() {
        return hands;
    }

    public void setHands(List<Hand> hands) {
        this.hands = hands;
    }

    public void addHand(Hand hand) {
        this.hands.add(hand);
    }

    public int getInitialBank() {
        return initialBank;
    }

    public void setInitialBank(int initialBank) {
        this.initialBank = initialBank;
        currentBank = initialBank;
    }

    public int getCurrentBank() {
        return currentBank;
    }

    public void setCurrentBank(int currentBank) {
        this.currentBank = currentBank;
    }

    public int getBet() {
        return bet;
    }

    public void setBet(int bet) {
        this.bet = bet;
    }

    public int getSideBet() {
        return sideBet;
    }

    public void setSideBet(int sideBet) {
        this.sideBet = sideBet;
    }

    public void splitHand(Dealer dealer, Hand hand) {
        Hand newHand = new Hand();
        newHand.addCardToHand(hand.getCards().get(1));
        hand.getCards().remove(1);
        newHand.setBetAmount(bet);
        currentBank -= bet;
        hands.add(newHand);
        dealer.dealCard(newHand);
        dealer.dealCard(hand);
    }
}