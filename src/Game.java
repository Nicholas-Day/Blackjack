import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

public class Game {
    public static BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
    private List<Player> players;
    private Dealer dealer;

    public static void printCard(Card card) {
        System.out.println("(" + card.getAbbrev() + ") " + card.getRank() + " of " + card.getSuit());
    }

    public static void displayHand(String name, Hand hand) {
        System.out.println(name + "'s cards:");
        for (Card currentCard : hand.getCards()) {
            printCard(currentCard);
        }
        System.out.println("The current value of your hand is: " + hand.getHandValue() + "\n");
    }

    public void displayTurnInfo(Player currentPlayer, Hand currentHand) {
        System.out.println("\nDealer's cards: \n(xx) face down");
        printCard(dealer.getHand().getCards().get(0));
        System.out.println("Dealer is showing a hand value of: " + dealer.getHand().getCards().get(0).getValue() +
                "\n");
        displayHand(currentPlayer.getName(), currentHand);
    }

    public void playTurn(Player currentPlayer) {
        for (int i = 0; i < currentPlayer.getHands().size(); i++ ) {
            Hand currentHand = currentPlayer.getHands().get(i);
            // Payout if player has a natural blackjack
            if (currentHand.is21() && (currentPlayer.getHands().size() == 1)) {
                displayTurnInfo(currentPlayer, currentHand);
                dealer.payout(currentPlayer, currentHand.getBetAmount(), 1.5d);
                System.out.println("You have blackjack! Your payout is: " + ((int) (currentHand.getBetAmount() * 1.5)));
                currentHand.setBetAmount(0);
                break;
            }
            // Otherwise play hand as usual
            else {
                int choice = 0;
                while (true) {
                    displayTurnInfo(currentPlayer, currentHand);
                    System.out.println("Do you want to: \n 1. Hit \n 2. Split Pair \n 3. Double down \n 4. Stand \n 5" +
                            ". Surrender");
                    try {
                        choice = Integer.parseInt(Game.reader.readLine());
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                    switch (choice) {
                        case 1:
                            dealer.dealCard(currentHand);
                            break;
                        case 2:
                            if ((currentHand.getCards().size() > 2)) {
                                System.out.println("You can only split your hand if you have two cards. Please " +
                                        "pick another option");
                                break;
                            }
                            if (!currentHand.hasPair()) {
                                System.out.println("You can only split your hand if you have a pair. Please pick " +
                                        "another option.");
                                break;
                            }
                            if (currentHand.getBetAmount() > currentPlayer.getCurrentBank()) {
                                System.out.println("You can only split hand if you can afford to match your " +
                                        "original bet. Please pick another option.");
                                break;
                            }
                            currentPlayer.splitHand(dealer, currentHand);
                            break;
                        case 3:
                            if ((currentHand.getCards().size() > 2) || (currentPlayer.getHands().size() > 1)) {
                                System.out.println("You can only double down on your initial two cards. Please " +
                                        "pick another option");
                                break;
                            }
                            // Get double down bet amount
                            System.out.print("\nYou can double down for less than you original bet of $" +
                                    currentPlayer.getBet() + " if you would like. \nHow much would you like " +
                                    "to double down for: $");
                            currentHand.setBetAmount(currentPlayer.getBet() + dealer.getBetFromPlayer(currentPlayer,
                                    currentPlayer.getBet()));

                            System.out.println("Your bet is now $" + currentHand.getBetAmount());

                            // Give player card
                            dealer.dealCard(currentHand);
                            break;
                        case 4:
                            break;
                        case 5:
                            currentPlayer.setCurrentBank(currentPlayer.getCurrentBank() + (currentPlayer.getBet() / 2));
                            System.out.println("You have surrendered, you lose half your bet, $" + ((currentPlayer.getBet() + 1) / 2));
                            currentHand.setBetAmount(0);
                            break;
                        default:
                            System.out.println("Please choose a valid option");
                    }
                    if (currentHand.getHandValue() > 21) {
                        displayHand(currentPlayer.getName(), currentHand);
                        System.out.println("You busted and lost your bet of $" + currentHand.getBetAmount());
                        currentHand.setBetAmount(0);
                        break;
                    }
                    if (currentHand.is21()) {
                        currentPlayer.setCurrentBank(currentPlayer.getCurrentBank() + currentPlayer.getBet());
                        displayHand(currentPlayer.getName(), currentHand);
                        System.out.println("You have blackjack!");
                        break;
                    }
                    if ((choice > 2) && (choice < 6)) break;
                }
            }
        }
    }

    public void playRound() {
        // Shuffle deck
        dealer.getDeck().shuffle();

        // Place bets
        for (Player currentPlayer : players) {
            System.out.println("\n" + currentPlayer.getName() + "'s bank: $" + currentPlayer.getCurrentBank());
            System.out.print("Place bet: $");
            currentPlayer.setBet(dealer.getBetFromPlayer(currentPlayer));
        }

        // Deal cards
        for (int i = 0; i < players.size(); i++) {
            Hand hand = new Hand();
            dealer.dealCard(hand);
            dealer.dealCard(hand, players.size() - i);
            hand.setHandValue(hand.determineValue());
            hand.setBetAmount(players.get(i).getBet());
            players.get(i).addHand(hand);
        }
        dealer.takeCard();
        dealer.takeCard();

        // Check for Ace and offer insurance
        if (dealer.getHand().getCards().get(0).getValue() == 11) {
            dealer.offerInsurance(players);
            // Pay insurance if dealer has blackjack
            if (dealer.getHand().is21()) {
                for (Player player : players) {
                    dealer.payout(player, player.getSideBet(), 3);
                    player.setSideBet(0);
                }
            }
        }

        // Check if dealer has natural blackjack
        if (dealer.getHand().is21()) {
            System.out.println("Dealer has blackjack:");
            displayHand(dealer.name, dealer.getHand());
            // Check if each player has blackjack
            for (Player currentPlayer : players) {
                if (currentPlayer.getHands().get(0).is21()) {
                    System.out.println("You also have blackjack:");
                    displayHand(currentPlayer.getName(), currentPlayer.getHands().get(0));
                    System.out.println("You get your bet back");
                    dealer.payout(currentPlayer, currentPlayer.getBet(), 0);
                }
                else {
                    System.out.println("You don't have blackjack, you lose your bet");
                    currentPlayer.setCurrentBank(currentPlayer.getCurrentBank() - currentPlayer.getBet());
                }
                System.out.println("Your current bank is: " + currentPlayer.getCurrentBank());
            }
        }
        // Otherwise each player takes their turn then the dealer takes its turn
        else {
            for (Player currentPlayer : players) {
                playTurn(currentPlayer);
            }
            System.out.println("Dealer's turn");
            dealer.playTurn();

            // Make remaining payouts
            if (dealer.getHand().getHandValue() > 21) {
                System.out.println("Dealer busted");
                for (Player currentPlayer : players) {
                    for (Hand currentHand : currentPlayer.getHands()) {
                        dealer.payout(currentPlayer, currentHand.getBetAmount(), 1);
                    }
                }
            }
            else {
                System.out.println("Dealer stands with " + dealer.getHand().getHandValue());
                for (Player currentPlayer : players) {
                    for (Hand currentHand : currentPlayer.getHands()) {
                        if (currentHand.getHandValue() > dealer.getHand().getHandValue()) {
                            dealer.payout(currentPlayer, currentHand.getBetAmount(), 1);
                        }
                    }
                }
            }
        }

        // Return player cards to deck
        for (Player currentPlayer : players) {
            for (Iterator<Hand> handIterator = currentPlayer.getHands().iterator(); handIterator.hasNext(); ) {
                for (Iterator<Card> cardIterator = handIterator.next().getCards().iterator(); cardIterator.hasNext(); ) {
                    dealer.getDeck().addCard(cardIterator.next());
                }
                handIterator.remove();
            }
        }
        // Return dealer cards to deck
        for (Card currentCard : dealer.getHand().getCards()) {
            dealer.getDeck().addCard(currentCard);
        }
        dealer.getHand().getCards().removeAll(dealer.getHand().getCards());
    }

    public void playGame() {
        while (true) {
            playRound();
        }
    }

    public void setupGame() {
        dealer = new Dealer();
        players = new ArrayList<>();

        System.out.print("How many players: ");
        try {
            dealer.setNumOfPlayers(Integer.parseInt(Game.reader.readLine()));
            // Get name and buy in for each player
            for (int i = 0; i < dealer.getNumOfPlayers(); i++) {
                String name = "newPlayer";
                String initBank = "0";
                try {
                    System.out.print("Enter name for player " + (i + 1) + ": ");
                    name = Game.reader.readLine();
                    System.out.print("Buy in: $");
                    initBank = Game.reader.readLine();
                } catch (IOException e) {
                    e.printStackTrace();
                }
                Player newPlayer = new Player();
                newPlayer.setInitialBank(Integer.parseInt(initBank));
                newPlayer.setName(name);
                players.add(newPlayer);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}