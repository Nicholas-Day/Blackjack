import java.util.List;

public class Dealer {
    public final String name = "Dealer";
    private Deck deck = new Deck();
    private Hand hand = new Hand();
    private int numOfPlayers;

    public Deck getDeck() {
        return deck;
    }

    public void setDeck(Deck deck) {
        this.deck = deck;
    }

    public Hand getHand() {
        return hand;
    }

    public void setHand(Hand hand) {
        this.hand = hand;
    }

    public int getNumOfPlayers() {
        return numOfPlayers;
    }

    public void setNumOfPlayers(int numOfPlayers) {
        this.numOfPlayers = numOfPlayers;
    }

    public int getBetFromPlayer(Player player) {
        int bet = 0;
        bet = Game.scanner.nextInt();
        // TODO replace 5 with a minimum bet constant
        if (bet < 5) bet = 5;
        if (bet > player.getCurrentBank()) bet = player.getCurrentBank();
        player.setCurrentBank(player.getCurrentBank() - bet);

        return bet;
    }

    public int getBetFromPlayer(Player player, int max) {
        int bet = 0;
        bet = Game.scanner.nextInt();
        // TODO replace 5 with a minimum bet constant
        if (bet < 0) bet = 0;
        if (bet > max) bet = max;
        if (bet > player.getCurrentBank()) bet = player.getCurrentBank();
        player.setCurrentBank(player.getCurrentBank() - bet);

        return bet;
    }

    public void takeCard() {
        hand.addCardToHand(deck.getCards().get(0));
        deck.getCards().remove(0);
    }

    public void dealCard(Hand hand) {
        hand.addCardToHand(deck.getCards().get(0));
        deck.getCards().remove(0);
    }

    public void dealCard(Hand hand, int index) {
        hand.addCardToHand(deck.getCards().get(index));
        deck.getCards().remove(index);
    }

    public void playTurn() {
        switch (hand.getHandValue()) {
            case 17:
                if (hand.hasAce()) {
                    while (hand.getHandValue() < 18) takeCard();
                }
            case 18, 19, 20, 21:
                break;
            default:
                while (hand.getHandValue() < 17) takeCard();
        }
    }

    public void offerInsurance(List<Player> players) {
        for (Player currentPlayer : players) {
            hand.display("Dealer", true);
            String choice = "bad";
            while (choice.equals("bad")) {
                System.out.println("Dealer is showing an Ace. Would you like to place an insurance side bet?\n" +
                        " 1. Yes\n 2.No");
                choice = Game.scanner.nextLine();
                switch (choice) {
                    case "1", "Y", "y", "YES", "Yes", "yes":
                        System.out.print("You can place an insurance side bet (paid at 2x) of up to $" +
                                (currentPlayer.getBet() / 2) + "\nHow much would you like to bet: $");
                        currentPlayer.setSideBet(getBetFromPlayer(currentPlayer, currentPlayer.getBet() / 2));
                    case "2", "n", "N", "NO", "No", "no":
                        break;
                    default:
                        choice = "bad";
                        System.out.println("Please enter a valid option.");
                }
            }

        }
    }

    public void payout(Player player, int betAmount, double payoutRate) {
        player.setCurrentBank((int) (player.getCurrentBank() + (betAmount * (payoutRate + 1))));
    }
}