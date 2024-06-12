## Hotel Playwright Tests

### 概要
ホテル予約サイトを模したテスト用のサイトを使用し、3つのテストケースを実行します。

テスト対象サイト: [ホテル予約サイト](https://hotel.testplanisphere.dev/ja/index.html)

### プログラミング言語
- C#

### フレームワーク
- Playwright

### テストケース

#### ログイン機能
1. **登録ユーザーでのログイン**  
   登録されたユーザーでログインが成功するかを確認します。
   
2. **未入力の場合のエラー表示の確認**  
   メールアドレスとパスワードが未入力の場合、適切なエラーメッセージが表示されることを確認します。

#### 宿泊プランの動作確認
1. **未ログイン状態の確認**  
   未ログイン状態で表示される宿泊プラン一覧を確認します。
   
2. **一般会員の確認**  
   一般会員でログインした状態で表示される宿泊プラン一覧を確認します。
   
3. **プレミアム会員の確認**  
   プレミアム会員でログインした状態で表示される宿泊プラン一覧を確認します。


### 依存関係

- [Microsoft.Playwright](https://playwright.dev/dotnet/docs/intro)

### 実行方法

1. **リポジトリをクローン**
    ```sh
    git clone https://github.com/yourusername/hotel-playwright-tests.git
    ```

2. **依存関係をインストール**
    ```sh
    cd hotel-playwright-tests/HotelPlaywrightTests/HotelPlaywrightTests
    dotnet restore
    ```

3. **テストの実行**
    ```sh
    dotnet test --filter "LoginPageTests"
    dotnet test --filter "PlansPageTests"
    ```
### 詳細

#### LoginPageTests.cs
このテストファイルでは、以下のテストケースを対象とする。
- **LoggedInDefineUser**: 登録ユーザーでのログインを確認する。
- **ErrorMessageempty**: メールアドレスとパスワードが未入力の場合のエラーメッセージが表示されることを確認する。

#### PlansPageTests.cs
このテストファイルでは、以下のテストケースを対象とする。
- **PlansNotLoggedIn**: 未ログイン時に正しい宿泊プランが表示されることを確認する。
- **PlansLoggedInGeneralMember**: 一般会員でログイン時に正しい宿泊プランが表示されることを確認する。
- **PlansLoggedInPremiumMember**: プレミアム会員でログイン時に正しい宿泊プランが表示されることを確認する。

### ライセンス
このプロジェクトはMITライセンスで公開されている。詳細については、`LICENSE`ファイルを参照。




