# KiepMeta1, 방과 후 학습

## [PM,기획 - 조여준]
 
아래의 내용에 영향이 있을 경우 먼저 상의하고 진행
( https://open.kakao.com/o/gJwbSz4e )

### 기획관련
 수업중에 배운 기술들을 기반으로하고 학생들 스스로 확장하며 실력을 올릴 수 있는 범위.
 
 Web service & Metaverse & Blockchain NFT를 융합하여 서비스할 수 있는 환경을 제작.
- Web service: NFT를 보상으로 하는 Quiz대회 주최 기능. 그에 필요한 Quiz data와 가상World의 여러 구성요소들을 배치하고 꾸밀 수 있게 기능 제공.
- Metaverse: Quiz, World 등의 data를 받아서 사용자에게 제공할 3d 환경을 구축. 그 속에서 사용자들이 실시간 활동을 할 수 있게 제공.
- Blockchain NFT: NFT를 발행, 우승자에게 NFT를 제공, NFT소유자 조회 등을 제공(Web service 환경에 통합되어 제공).
- Etc: 실물을 사용자에게 제공(미정). 
 

#### [참고화면 Minecraft]

![MetaEx1-Minecraft1](https://user-images.githubusercontent.com/104874755/224620237-a16af7ef-30ec-4f96-91fe-f48fc31661a3.png)

 
 
#### [NftProject용 Quiz대회]
- 공통
  
  완료일정: 3.21일
  
  발표 연습 & 녹화: 3.22일
  
- Webservice

  NFT발행 "제x회 Quiz대회 우승 트로피" + 고유 트로피 image지정.
  
  대회 시작 시간 지정.
  
  Quiz 문제들 입력.
 
  NFT id 또는 사용자 이름으로 NFT정보 조회(NFT이름, 소유자, image 등)

- Metaverse

  NFT정보(이름, 이미지(고정 또는 고유image))
 
  Quiz대회 정보(시작시간, NFT 등) / Quiz진행

- NFT

  수업 중 진행...
 
 
 
 
### 개발관련
- git에 올리기전 확인 받을 것(임시 공유는 압축파일로 필요한 사람에게 공유).
- 서로 하는 일 알 수 있게 '작업일지' 작성.
- 함수이름: class소속 함수는 소문자로 시작, 일반 또는 static 함수는 대문자로 시작.
- 변수이름: 변수는 소문자로 시작, 상수는 대문자로 시작.
- class이름: class정의는 대문자로 시작.
 
## [개발,구현 - 학생들로 구성]
 (출석부순) 리진, 권창범, 김규환, 반현진, 송두영, 이동규
- Web
- Metaverse
- Blockchain
- Etc

## [작업 목록, 시간, 상황]
전체 작업에서 허점을 줄이려면 서로 생각하는 작업 공유가 중요!.

### 이동규
- webserver와 db연결 작업 1일, 완료
- web 꾸미기 작업 2일, 완료
- web quiz data db에 채우기 1일, 완료
- web과 server 연결 작업 1일, 완료
- 로그인 화면 구성 3일, 미완

### 리진
- 서버 연동 (캐릭터 실시간 위치, 상태 등): 1일, 완료.
- Unity 지도 생성: 1일. 완료
- DB에서 상태 저장, 1일, 진행중.
- NFT 발행: 1일, 미완료.
- 게임 UI 디자인, 1일, 미완료.

### 반현진
- 웹 서버 db 연결 1일, 완료
- 웹 quiz데이터 db에 연결 및 저장 1일, 완료
- login 및 데이터 저장 2~3, 미완 
- 웹 페이지 꾸미기 2일, 미완
- quiz데이터를 unity에 연결해서 출력 2~3일, 미완
- Unity 맵 구상 및 구현 5~6일, 미완
- 웹, Unity 등 사용해서 퀴즈 보상(NFT) 주기 3~5일, 미완

### 김규환
- 플레이어 이동 및 모션 구현 1일 완료
- 퀴즈 UI 및 db 연결 2일 미완 
- 퀴즈 진행 및 구성 2일 미완
- 퀴즈 디자인 및 기능 수정 1.5일 미완
- 플레이어 모델링 수정 1일 미완
- NFT 발행 1일 미완

# 방과 후 작업 일지


## 20230221

리진: 이날 Main1 의 Update() 함수에서 캐릭터 실시간 위치(x,z)는  NECIAL 서버에 정송 성공했슴.
   그러나 캐릭터 가만히 있을때 도 계속 촤표를 전송하고 있고요. 네트워크 자원 낭비 됐어요.
   그래서 CharacterController에서 캐릭터 움직임을 판단하고, 수정 후 움직일 때만 서버에서 전송 구현 성공. 

## 20230222
리진: 이날 게임 중에 키보드로 UI 화면을 호출하고, Save 버튼을 누르면 현재 이름, 위치 정보를 DB에서 저장 완료.
   이 기능을 통해 게임 다시 로딩할 때, DB로 현재 상태를 복구할 수 있습니다.
   
## 20230223
리진:  지도는 무작위의 일정한 높낮이에 따라 생성하고, 그리고 랜덤하게 보물함을 추가했어요. 

## 20230224
리진:  지도 주면 높은 벽들을 세웠어요. 직접 Mesh를 생성했어요.

## 20230303
김규환 : 유니티 플레이어 점프시 바닥태그("Floor") 설정 필수 (태그 추가 필요시 PlayerMotion코드 - OnCollisionEnter의 조건문에 추가)

## 20230314
금요일 17일, 화요일 21일 완료 내용 계획 필요.
  ### 금요일17일 오전까지 완료
    리진: 웹으로 NFT 발행및 조회.
    권창범: fff
    김규환: 퀴즈 작동 및 NFT 받을수 있는 환경(코드) 
    반현진: 퀴즈데이터 연동 및 입,출력 구현
    송두영: jjj
    이동규: 웹 서비스 대회 시간 지정, 퀴즈 문제들 입력 구현
  ### 화요일21일 완료
    리진: asdf
    권창범: fff
    김규환: ggg
    반현진: hh
    송두영: jjj
    이동규: 대회 시작 시간 및 퀴즈 문제 DB에 저장, 유니티 서버와 연결
  ### 수요일22일 발표연습과 녹화(예정)

## 20230315
