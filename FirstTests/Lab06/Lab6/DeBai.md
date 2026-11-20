# LAB 6 – TEST RUNNER, BÁO CÁO, LOGGING & CHẠY SONG SONG

Hình thức nộp: Repository GitHub public – làm tiếp trên repo Lab 1–5.

## Mục tiêu
- Phân nhóm test NUnit bằng `[Category]` và chạy chọn lọc.
- Logging có dấu thời gian và mức độ trong BaseTest.
- Xuất TRX, chuyển đổi thành báo cáo HTML.
- Chạy song song có kiểm soát, đánh giá thread-safety.
- Dùng AI sinh assertions/cases và thẩm định trước khi dùng.

## Yêu cầu (100%)

### Phần A – Tổ chức test với [Category] (15%)
- Gắn [Category] cho ít nhất 2 nhóm, ví dụ Smoke và Login.
- Chạy chọn lọc: dotnet test --filter "Category=Smoke". Chụp ảnh chứng minh chỉ ca thuộc nhóm đó chạy.
- Chạy lọc loại trừ, ví dụ --filter "Category!=Slow". Chụp ảnh minh chứng.
- Đưa lên repository GitHub public đang làm tiếp từ Lab 1-5.

### Phần B – Logging trong BaseTest (25%)
- Viết helper Log có tối thiểu Info và Error, mỗi dòng có dấu thời gian và mức độ.
- Bắt đầu test ghi Info ở [SetUp], kết thúc ghi OK/RỚT ở [TearDown].
- Test không tự ghi log; log đến từ lớp BaseTest.
- Có đoạn log thực tế gồm ca Passed và ít nhất một ca Failed/ERROR.
- Dùng TestContext.Progress.WriteLine; có thể dotnet test -l "console;verbosity=detailed".

### Phần C – Báo cáo HTML từ TRX (15%)
- Chạy dotnet test --logger "trx;LogFileName=ketqua.trx" để tạo dữ liệu TRX.
- Dùng công cụ như trxlog2html để chuyển đổi TRX -> báo cáo HTML.
- Chụp hình báo cáo HTML có số test, số Passed/Failed.
- Bản HTML tạo trực tiếp từ NUnit khi Run Visual Studio là xem nhanh; để nộp tiêu chí này cần minh chứng TRX -> HTML riêng.

### Phần D – Chạy song song và thread-safety (30%)
- Đo tổng thời gian chạy tuần tự, lưu ảnh và kết quả.
- Bật assembly Parallelizable(ParallelScope.Fixtures) và LevelOfParallelism(N).
- Đo tổng thời gian chạy song song, lưu ảnh và kết quả.
- Đối chiếu cùng tập ca; khẳng định kết quả không đổi (không có test rớt thêm).
- Giải thích 2-3 câu về biến driver của mỗi fixture, tránh static/shared state.
- Khuyến nghị bật Headless=true khi chạy nhiều trình duyệt.

### Phần E – AI sinh và thẩm định test/assertion (15%)
- Chọn một trường nhập thực tế như username/password.
- Nhờ AI đề xuất test case và NUnit Assert, có ca biên.
- Lưu nguyên prompt và toàn bộ output AI thật.
- Chỉ rõ ít nhất hai trường hợp AI đề xuất không phù hợp và lý do theo hành vi thật.
- Commit theo Conventional Commits.

## Gợi ý kỹ thuật của đề
1. In nhật ký: `dotnet test -l "console;verbosity=detailed"`.
2. Dùng `TestContext.Progress.WriteLine`, không bắt buộc thư viện logging ngoài.
3. LevelOfParallelism không đặt cao quá tài nguyên CPU/trình duyệt.
4. Headless=true giúp tiết kiệm tài nguyên khi parallel.
5. Ca Failed ngẫu nhiên là dấu hiệu có trạng thái dùng chung cần kiểm tra.
6. Ưu tiên `ParallelScope.Fixtures` trước khi cân nhắc `Children`.
7. Data-Driven của Lab 5 vẫn dùng được; phải lưu ý tính độc lập giữa các ca.
8. Commit theo Conventional Commits.

## Sản phẩm nộp
- Link repository GitHub public, nối tiếp Lab 1–5.
- screenshots/ cho filter Category (A), logging (B), HTML (C), so sánh thời gian (D).
- README.md: giải thích thread-safety (D) và thẩm định AI (E).

## Lưu ý
Bài yêu cầu nhanh mà vẫn đúng: nếu chạy song song làm kết quả thay đổi thì chưa đạt.
