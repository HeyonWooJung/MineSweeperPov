# MineSweeperPov
 캐릭터를 움직여서 하는 지뢰찾기
---
 ## 프로젝트명/제작기간
 - 프로젝트명: 지뢰제거반
 - 제작기간: 12/23 ~ 12/27(총 5일)
---
 ## 개요
- 지뢰찾기 룰이지만 캐릭터를 직접 움직여 진행하는 게임
- 기존 지뢰찾기와 동일하게 지뢰를 모두 찾으면 클리어, 지뢰를 밟으면 게임오버
- 플레이어 주변 1칸만 숫자가 보임
---
 ## 주요 기능
 - 타이틀화면에서의 메뉴(시작, 룰, 종료)
 - 맵의 생성 (?x?의 칸에 n개의 지뢰)
 - 가능하면 난이도 설정도 구현
 - 남은 지뢰의 수와 경과한 시간 표시
 - 플레이어의 이동(wasd나 방향키로 이동)
 - 플레이어가 이동한 곳 주변에 지뢰가 없는 곳은 모두 밝힘
 - 플레이어가 바라보는 방향에 깃발을 꽂는 기능
 - 플레이어 주변의 1칸에 지뢰 수를 표시
 - 모든 지뢰에 깃발이 꽂히거나 지뢰를 제외한 모든 위치를 밝히면 게임 승리, 플레이어가 지뢰를 밟으면 게임오버
---
 ## 게임 흐름
 - 게임 시작 화면
 - 난이도 설정(사용자 설정 난이도) <- 가능하면 구현하기
 - n * n 크기의 맵을 생성 후 임의의 위치에 m개의 지뢰를 심음
 - 플레이어는 주변 1칸의 지뢰 수를 확인하며 맵을 탐색함
 - 플레이어가 지뢰를 밟았다면 게임오버, 플레이어가 지뢰를 모두 피해 깃발을 박거나 맵을 밝히면 승리
 - 결산화면에 경과 시간, 남은 지뢰(게임오버라면) 등 표시
 - ---
 ## 사용 데이터 구조
 - struct : 주변 지뢰의 수, 자신의 지뢰 여부, 밝혀진 위치인지 여부를 저장할 구조체 생성
 - 2차배열 : 위 구조체를 2차배열로 만들어 맵을 그릴 예정, 맵의 크기를 정해놓고 생성할 것이기 때문에 고정 사이즈인 배열이 더 좋을거라 생각
 ---
 ## 주요 클래스
 - Player : 플레이어의 이동과 행동을 구현 예정
 - GameManager : 게임의 시작, 종료 시점의 판단, 맵의 그리기, 경과 시간과 남은 지뢰 수를 표시하게 구현 예정
 - MineManager : 맵을 생성하며 지뢰를 놓고 주변 지뢰의 수를 저장하는 기능 구현 예정
---
## 기술적 요구사항
- 주석 열심히 달기
- 가능하면 상속이나 추상 클래스, 제네릭 적극적으로 활용해 코드 중복 없애기
- 객체 지향 원칙에 맞게 개발하기
 ---
## 추가 구현 아이디어
- 난이도 설정 기능
- 사용자 지정 난이도 설정 기능
---
## 개발 일정
 - Day 1: 프로젝트 기획 및 클래스 설계, 기초 구현
 - Day 2: 핵심 기능 구현
 - Day 3: 추가 기능 구현
 - Day 4: 기능 테스트 및 그래픽? 추가
 - Day 5: 최종 테스트 및 발표 자료 준비
---
## Git 전략
- 기능마다 브랜치를 파서 완성되면 합치기
- 세부구현마다 커밋하고 전체 완성/집 가기전에 푸시
