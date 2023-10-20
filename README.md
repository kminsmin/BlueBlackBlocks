<img src="https://github.com/kminsmin/BlueBlackBlocks/assets/70641418/5d98ee35-b514-472d-bcd7-bb2e1fe98c33">

# BlueBlackBlocks(941生, A09조)

<img src="https://github.com/kminsmin/BlueBlackBlocks/assets/70641418/7cd85133-dad5-4989-8ccf-07be054dd778">

1 ~ 2인 멀티 플레이가 가능한 2D 어드벤쳐 게임입니다.

<br/>

# 시연 영상
---

[![Video Label](http://img.youtube.com/vi/SF5HudJucuU/0.jpg)](https://youtu.be/SF5HudJucuU)

(클릭하면 유튜브 영상 링크로 넘어갑니다.)

<br/>

# 팀원/ 담당 파트
---

|이름|담당 업무|깃허브 링크|
|------|---|---|
|이경민(팀장)|레벨 디자인|[링크](https://github.com/kminsmin)|
|신우석|플레이어, 클라이언트 동기화|[링크](https://github.com/seoksii)|
|한병권|UI|[링크](https://github.com/hbg9212)|
|임전혁|사운드, 로그인, 회원가입|[링크](https://github.com/yarogono)|


<br/>


# 기술 스택

<table>
	<tr>
		<th>분류</th>
		<th>기술</th>
	</tr>
	<tr align="center">
		<td>Language</td>
		<td><img src="https://img.shields.io/badge/CSharp-purple?style=for-the-badge&logo=csharp&logoColor="></td>
	</tr>
	<tr align="center">
		<td>Game Engine</td>
		<td><img src="https://img.shields.io/badge/Unity-black?style=for-the-badge&logo=unity&logoColor=white"></td>
	</tr>
	<tr align="center">
		<td>Server</td>
		<td><img src="https://img.shields.io/badge/ASP.NET Core-blue?style=for-the-badge&logo=.net&logoColor=white"></td>
	</tr>
	<tr align="center">
		<td>DB</td>
		<td><img src="https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=MySQL&logoColor=white"></td>
	</tr>
	<tr align="center">
		<td>Other</td>
		<td><img src="https://img.shields.io/badge/Entity Framework Core-purple?style=for-the-badge"><br>
		    <img src="https://img.shields.io/badge/Nginx-green?style=for-the-badge&logo=nginx&logoColor=white">
		</td>
	</tr>
</table>

<br/>


# 아키텍처(Architecture)

<img src="https://github.com/kminsmin/BlueBlackBlocks/assets/70641418/430d2ede-6bee-4798-b70d-44e9a45b539a">

- 유니티 클라이언트에서 포톤을 사용해 캐릭터 및 애니메이션 동기화 구현
- 클라이언트(유니티)에서 ASP.NET Core 웹 애플리케이션으로 Request를 보내고 Response를 받습니다.
- nginx를 프록시 역할을 통해 8080포트로 접근하면 5000포트를 사용하고 있는 ASP.NET Core 앱에 패킷을 전송합니다.
- ASP.NET Core에 전송된 패킷 데이터를 바탕으로 MySQL DB에서 데이터를 조회, 삽입, 수정을 합니다.
- Ubuntu는 여분의 컴퓨터에 서버용 컴퓨터를 세팅했습니다.
  - 포트포워딩을 통해 8080 포트를 열어놨습니다.

<br/>



# 프로젝트 회고(블로그 글)
- Unity 기존 게임을 포톤 PUN2로 동기화하기 => [블로그 링크](https://seoksii.tistory.com/64)
- Unity 포톤 동기화 작업 시 주의할 점들 => [블로그 링크](https://seoksii.tistory.com/65)
- Unity 포톤으로 멀티플레이 구현해보기 => [블로그 링크](https://seoksii.tistory.com/63)

