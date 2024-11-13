import pygame
import random
import sys

# Initialize Pygame
pygame.init()

# Screen dimensions
WIDTH, HEIGHT = 800, 600
screen = pygame.display.set_mode((WIDTH, HEIGHT))
pygame.display.set_caption("Space Shooting Game")

# Load images
background_image = pygame.image.load("background.png")
background_image = pygame.transform.scale(background_image, (WIDTH, HEIGHT))
spaceship_image = pygame.image.load("spaceship.png")
asteroid_image = pygame.image.load("asteroid.png")

# Colors
WHITE = (255, 255, 255)
BLACK = (0, 0, 0)

# Game settings
initial_asteroid_spawn_time = 1000  # Initial spawn time in milliseconds
spawn_time_decrease = 20            # Time decrease in milliseconds to increase difficulty
min_asteroid_spawn_time = 300       # Minimum spawn time

class GameObject:
    """Base class for all game objects."""
    def __init__(self, image, x, y):
        self.image = image
        self.rect = self.image.get_rect(topleft=(x, y))
    
    def draw(self, surface):
        surface.blit(self.image, self.rect)

class Spaceship(GameObject):
    """Spaceship controlled by the player."""
    def __init__(self, x, y):
        image = pygame.transform.scale(spaceship_image, (50, 38))
        super().__init__(image, x, y)
        self.speed = 5
    
    def move(self, keys):
        if keys[pygame.K_LEFT] and self.rect.left > 0:
            self.rect.x -= self.speed
        if keys[pygame.K_RIGHT] and self.rect.right < WIDTH:
            self.rect.x += self.speed
        if keys[pygame.K_UP] and self.rect.top > 0:
            self.rect.y -= self.speed
        if keys[pygame.K_DOWN] and self.rect.bottom < HEIGHT:
            self.rect.y += self.speed

class Bullet(GameObject):
    """Bullet shot by the spaceship."""
    def __init__(self, x, y):
        bullet_surface = pygame.Surface((5, 10))
        bullet_surface.fill(WHITE)
        super().__init__(bullet_surface, x, y)
        self.speed = 7

    def update(self):
        self.rect.y -= self.speed

class Asteroid(GameObject):
    """Asteroid that the spaceship needs to avoid and shoot."""
    def __init__(self):
        image = pygame.transform.scale(asteroid_image, (100, 100))
        x = random.randint(0, WIDTH - image.get_width())
        y = -image.get_height()  # Start above the screen
        super().__init__(image, x, y)
        self.speed = random.randint(3, 6)
    
    def update(self):
        self.rect.y += self.speed

class Game:
    """Main game class that handles game logic, updates, and rendering."""
    def __init__(self):
        self.spaceship = Spaceship(WIDTH // 2, HEIGHT - 60)
        self.bullets = []
        self.asteroids = []
        self.score = 0
        self.font = pygame.font.Font(None, 36)
        self.clock = pygame.time.Clock()
        self.running = True
        self.asteroid_spawn_time = initial_asteroid_spawn_time
        self.last_asteroid_time = pygame.time.get_ticks()
    
    def spawn_asteroid(self):
        # Increase spawn rate as time passes
        current_time = pygame.time.get_ticks()
        if current_time - self.last_asteroid_time > self.asteroid_spawn_time:
            self.last_asteroid_time = current_time
            self.asteroids.append(Asteroid())
            if self.asteroid_spawn_time > min_asteroid_spawn_time:
                self.asteroid_spawn_time -= spawn_time_decrease

    def handle_events(self):
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                self.running = False
            elif event.type == pygame.KEYDOWN:
                if event.key == pygame.K_SPACE:
                    bullet = Bullet(self.spaceship.rect.centerx - 2, self.spaceship.rect.top)
                    self.bullets.append(bullet)
    
    def update(self):
        keys = pygame.key.get_pressed()
        self.spaceship.move(keys)
        
        # Update bullets
        for bullet in self.bullets[:]:
            bullet.update()
            if bullet.rect.bottom < 0:
                self.bullets.remove(bullet)

        # Spawn and update asteroids
        self.spawn_asteroid()
        for asteroid in self.asteroids[:]:
            asteroid.update()
            if asteroid.rect.top > HEIGHT:
                self.asteroids.remove(asteroid)
        
        # Check for collisions
        self.check_collisions()

    def check_collisions(self):
        for asteroid in self.asteroids[:]:
            if self.spaceship.rect.colliderect(asteroid.rect):
                self.running = False  # End game if spaceship hits an asteroid
            for bullet in self.bullets[:]:
                if bullet.rect.colliderect(asteroid.rect):
                    self.bullets.remove(bullet)
                    self.asteroids.remove(asteroid)
                    self.score += 1

    def draw(self):
        screen.blit(background_image, (0, 0))  # Draw background
        self.spaceship.draw(screen)
        for bullet in self.bullets:
            bullet.draw(screen)
        for asteroid in self.asteroids:
            asteroid.draw(screen)

        # Display score
        score_text = self.font.render(f"Score: {self.score}", True, WHITE)
        screen.blit(score_text, (10, 10))

    def show_start_screen(self):
        # Display start screen
        screen.fill(BLACK)
        title_text = self.font.render("Space Shooting Game", True, WHITE)
        screen.blit(title_text, (WIDTH // 2 - title_text.get_width() // 2, HEIGHT // 2 - 50))
        prompt_text = self.font.render("Press ENTER to Start", True, WHITE)
        screen.blit(prompt_text, (WIDTH // 2 - prompt_text.get_width() // 2, HEIGHT // 2 + 20))
        pygame.display.flip()
        
        # Wait for the player to start
        waiting = True
        while waiting:
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    pygame.quit()
                    sys.exit()
                if event.type == pygame.KEYDOWN:
                    if event.key == pygame.K_RETURN:
                        waiting = False

    def show_game_over_screen(self):
        # Display game over screen
        screen.fill(BLACK)
        game_over_text = self.font.render("Game Over", True, WHITE)
        screen.blit(game_over_text, (WIDTH // 2 - game_over_text.get_width() // 2, HEIGHT // 2 - 50))
        score_text = self.font.render(f"Your Score: {self.score}", True, WHITE)
        screen.blit(score_text, (WIDTH // 2 - score_text.get_width() // 2, HEIGHT // 2 + 10))
        prompt_text = self.font.render("Press ENTER to Restart or ESC to Quit", True, WHITE)
        screen.blit(prompt_text, (WIDTH // 2 - prompt_text.get_width() // 2, HEIGHT // 2 + 70))
        pygame.display.flip()

        # Wait for the player to restart or quit
        waiting = True
        while waiting:
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    pygame.quit()
                    sys.exit()
                if event.type == pygame.KEYDOWN:
                    if event.key == pygame.K_RETURN:
                        waiting = False
                        self.reset()
                    elif event.key == pygame.K_ESCAPE:
                        pygame.quit()
                        sys.exit()
    
    def reset(self):
        # Reset game variables
        self.spaceship.rect.center = (WIDTH // 2, HEIGHT - 60)
        self.bullets.clear()
        self.asteroids.clear()
        self.score = 0
        self.asteroid_spawn_time = initial_asteroid_spawn_time
        self.last_asteroid_time = pygame.time.get_ticks()
        self.running = True

    def run(self):
        self.show_start_screen()  # Show the start screen

        while True:
            self.running = True
            while self.running:
                self.handle_events()
                self.update()
                self.draw()
                pygame.display.flip()
                self.clock.tick(60)
            self.show_game_over_screen()  # Show game over screen on loss

# Run the game
if __name__ == "__main__":
    game = Game()
    game.run()

# Quit Pygame
pygame.quit()
sys.exit()