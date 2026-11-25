
export interface LoginCommand {
  email: string;
  password: string;
  fingerprint?: string | null;
}


export interface LoginCommandDto {
  accessToken: string;
  refreshToken: string;

  expiresAtUtc: string;

  accessTokenExpiresAtUtc: string;

  refreshTokenExpiresAtUtc?: string;
}


export interface RefreshTokenCommand {
  refreshToken: string;
  fingerprint?: string | null;
}


export interface RefreshTokenCommandDto {
  accessToken: string;
  refreshToken: string;

  accessTokenExpiresAtUtc: string;

  refreshTokenExpiresAtUtc: string;
}


export interface LogoutCommand {
  refreshToken: string;
}
export interface RegisterCommand {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  gender: 'male' | 'female';
}
