[![Playwright Tests and Report](https://github.com/Bey9434/hotel-playwright-tests/actions/workflows/test-run-report-deploy.yml/badge.svg)](https://github.com/Bey9434/hotel-playwright-tests/actions/workflows/test-run-report-deploy.yml)

## Hotel Playwright Tests

### 概要

テスト対象サイト: [ホテル予約サイト](https://hotel.testplanisphere.dev/ja/index.html)

### プログラミング言語

- C#

### フレームワーク

- Playwright

### テストケース

ホテル予約サイトを模したテスト用のサイトを使用し、5つのテストケースを実行する。

#### ログイン機能

1. **登録ユーザーでのログイン**  
   登録されたユーザーでログインが成功するかを確認する。
2. **未入力の場合のエラー表示の確認**  
   メールアドレスとパスワードが未入力の場合、適切なエラーメッセージが表示されることを確認する。

#### 宿泊プランの動作確認

3. **未ログイン状態の確認**  
   未ログイン状態で表示される宿泊プラン一覧を確認する。
4. **一般会員の確認**  
   一般会員でログインした状態で表示される宿泊プラン一覧を確認する。
5. **プレミアム会員の確認**  
   プレミアム会員でログインした状態で表示される宿泊プラン一覧を確認します。

### 必要な環境

- [Microsoft.Playwright](https://playwright.dev/dotnet/docs/intro)
- .NET SDK: バージョン8.0以降
- Visual Studio Code (推奨)

### 実行方法

1. **リポジトリをクローン**

   ```sh
   git clone https://github.com/Bey9434/hotel-playwright-tests
   ```

2. **依存関係をインストール**

   HotelPlaywrightTests.csprojが存在するディレクトリに移動して以下のコマンドを実行する。

   ```sh
   dotnet restore
   ```

   ※.NETのバージョンが8.0以外の場合、.csproj内の`<TargetFramework>`を使用しているバージョンに合わせて変更すること。

3. **プロジェクトのビルド**

   ```sh
   dotnet build
   ```

4. **Playwrightブラウザのインストール**

   ```sh
   pwsh bin/Debug/net8.0/playwright.ps1 install
   ```

   ※別のバージョンの.NETを使用している場合、/net8.0/をバージョンに合わせて調整すること。

5. **テストの実行**

   ```sh
   dotnet test --filter "LoginPageTests"
   dotnet test --filter "PlansPageTests"
   ```

### 詳細と注意点

#### 1. **LoginPageTests.cs**

このテストファイルでは、以下のテストケースを対象とする。

- LoggedInDefineUser: 登録ユーザーでのログインを確認する。
- ErrorMessageempty: メールアドレスとパスワードが未入力の場合のエラーメッセージが表示されることを確認する。

#### 2. **PlansPageTests.cs**

このテストファイルでは、以下のテストケースを対象とする。

- PlansNotLoggedIn: 未ログイン時に正しい宿泊プランが表示されることを確認する。
- PlansLoggedInGeneralMember: 一般会員でログイン時に正しい宿泊プランが表示されることを確認する。
- PlansLoggedInPremiumMember: プレミアム会員でログイン時に正しい宿泊プランが表示されることを確認する。

### pre-commitの導入

pre-commitはコミット前に自動でコードのチェックやテストを実施し、品質を保つためのツールである。

この設定では、JSONやYAMLの構文チェック、C#のコードフォーマット、テストを実行をする機能を実装している。

1. **pre-commitのインストール**

   以下のコマンドを実施し、pre-commitをインストールする。

   ```sh
   pip install pre-commit
   ```

2. **Pre-commit フックの有効化**

   プロジェクトディレクトリに移動して。pre-commitをインストールする。

   ```sh
   cd /path/to/your/hotel-playwright-tests
   pre-commit install
   ```
