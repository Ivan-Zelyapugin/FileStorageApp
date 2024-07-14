CREATE TABLE IF NOT EXISTS files (
    "id" uuid PRIMARY KEY,
    "userId" uuid NOT NULL,
    "fileName" VARCHAR(255) NOT NULL,
    "fileType" VARCHAR(255) NOT NULL,
    "fileSize" BIGINT NOT NULL,
    "fileUrl" VARCHAR(255) NOT NULL,
    "uploadedOn" TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    "expiryDate" TIMESTAMP,
    "isSingleUse" BOOLEAN NOT NULL,
    "isDownloaded" BOOLEAN NOT NULL
);
