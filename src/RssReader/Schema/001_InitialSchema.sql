CREATE TABLE IF NOT EXISTS User (
	Id integer PRIMARY KEY,
	Name text NOT NULL,
	Email text NOT NULL
);

CREATE TABLE IF NOT EXISTS Feed (
	Id integer PRIMARY KEY,
	FeedUrl text NOT NULL,
	LastSyncDateUTC text NOT NULL
);

CREATE TABLE IF NOT EXISTS Article (
	Guid text PRIMARY KEY,
	FeedId integer NOT NULL,
	Title text NOT NULL,
	Description text NOT NULL,
	Link text NOT NULL,
	PublishDate text NOT NULL,
	FOREIGN KEY (FeedId) REFERENCES Feed (Id)
);

CREATE TABLE IF NOT EXISTS Subscription (
	UserId integer NOT NULL,
	FeedId integer NOT NULL,
	PRIMARY KEY (UserId, FeedId),
	FOREIGN KEY (UserId) REFERENCES User (Id),
	FOREIGN KEY (FeedId) REFERENCES Feed (Id)
);
